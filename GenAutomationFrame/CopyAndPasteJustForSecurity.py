import os
import re


dest_dir = "C:\\Ella\\project\\pukeko\\microservice\\extension\\extensions-service\\Plexure.Extensions.ComponentTest\\case"
scan_dir = "C:\\Ella\\project\\pukeko\\microservice\\extension\\extensions-service\\Plexure.Extensions\\Controllers\\"
namespace=scan_dir.split('\\')[-2]
print namespace
operation_pattern = re.compile(r'SwaggerOperation\(\"(.*?)\"')
response_pattern =  re.compile(r'ResponseType\(typeof\((.*)\)\)')
parameter_pattern =  re.compile(r'.*IHttpActionResult.*\((.*)\)')
template_file = "C:\Ella\project\pukeko\copyAndPasteTool\stepTemplate.cs"
feature_template_file = "C:\\Ella\\project\\pukeko\\copyAndPasteTool\\template.feature"
template_file_handle = open(template_file, 'r')
template_file_content = template_file_handle.read()
template_file_handle.close()
feature_template_file_handle = open(feature_template_file, 'r')
feature_template_file_content = feature_template_file_handle.read()
feature_template_file_handle.close()


namespace_pattern = re.compile(r'NAMESPACE')
classname_pattern = re.compile(r'CLASSNAME')
classname_raw_pattern = re.compile(r'CLASSNAMERAW')
return_pattern =  re.compile(r'RETURNETYPE')
request_pattern =  re.compile(r'REQUEST')
method_pattern =  re.compile(r'METHOD')
parame_pattern = re.compile(r'ENTITY')
feature_pattern =  re.compile(r'FEATURENAME')
parameter_entity=""
special_pattern = re.compile(r',')
#scan_file_list=['C:\\Ella\\project\\pukeko\\microservice\\security\\security-service\\Plexure.Service.Security\\Controllers\\EmailTemplateController.cs']

for scan_file in os.listdir(scan_dir):
#for scan_file in scan_file_list:
    scan_file_handle = open(scan_dir+"\\"+scan_file, 'r')
    #scan_file_handle = open(scan_file, 'r')
    #print scan_file
    scan_info = scan_file_handle.read()
    operation_result = operation_pattern.findall(scan_info)
    print 'oper',operation_result
    response_result = response_pattern.findall(scan_info)
    #print response_result
    parameter_result = parameter_pattern.findall(scan_info)
    ##parameter_result = ['Guid accountId, AccountResourceUpsertRequest createRequest', 'Guid accountId, AccountResourceInstanceRequest deleteRequest','Guid accountId, [FromUri]AccountResourcePageRequest pageInfo','Guid accountId,string resourceId, [FromUri][Required]string resourceType','Guid accountId, AccountResourceUpsertRequest updateRequest',]
    print 'result:',parameter_result
    #generate feature_file and step defination
    for i in range(0,len(operation_result)-1):
        parameter_entity=""

        classname = operation_result[i][0].capitalize()+operation_result[i][1:]+"Steps"
        classname_raw =  operation_result[i][0].capitalize()+operation_result[i][1:]
        if i < len(response_result):		        
            returnType = response_result[i]		
        else:		
            print "response not match!"		
            returnType = "void"
        parameter = parameter_result[i]
        method = operation_result[i][0].capitalize()+operation_result[i][1:]
        if parameter != "":
            paras = parameter.split(',')
        else:
            para = []
        print paras
        for para in paras:
            if parameter_entity != "":
                parameter_entity = parameter_entity+","+para.split(" ")[-1]
            else:
                parameter_entity = para.split(" ")[-1]
        print parameter_entity
        step_file_name = dest_dir+"\\steps\\"+classname + ".cs"
        step_file_content = namespace_pattern.sub(namespace, template_file_content)
        step_file_content = classname_pattern.sub(classname, step_file_content)
        step_file_content =  return_pattern.sub(returnType, step_file_content)
        step_file_content = request_pattern.sub(parameter, step_file_content)
        step_file_content = method_pattern.sub(method, step_file_content)
        step_file_content = classname_raw_pattern.sub(classname_raw, step_file_content)
        if(parameter_entity !=""):
            step_file_content = parame_pattern.sub(parameter_entity, step_file_content)
        else:
            step_file_content = special_pattern.sub(parameter_entity, step_file_content)
        step_file = open(step_file_name, 'w')
        step_file.write(step_file_content)
        step_file.flush()
        step_file.close()
        #feature
        feature_file_handle = open(dest_dir+"\\feature\\"+operation_result[i][0].capitalize()+operation_result[i][1:]+".feature", 'w')
        feature_file_content = feature_pattern.sub(classname_raw, feature_template_file_content)
        feature_file_handle.write(feature_file_content)
        feature_file_handle.flush()
        feature_file_handle.close()
        
    
        
        
        
        
        
    

    
       
    
    
