using NAMESPACE.Sdk;
using NAMESPACE.Sdk.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace NAMESPACE.ComponentTest
{
    [Binding]
    public class CLASSNAME
    {
       public  CommonSteps commonSteps = new CommonSteps();
       
        public RETURNETYPE METHOD<T>(REQUEST, out T apierror) where T : class
        {
            commonSteps.GetTestSetting();
            string token = FeatureContext.Current.Get<string>("token");
            var client = SdkClientHelper.GetAuthenticatedClient(token, FeatureContext.Current.Get<string>("url"));
            var callResponse = client.CallApiWrappingResult<RETURNETYPE, T>(() => client.METHOD(ENTITY));
            apierror = callResponse.TypedError;
            callResponse.SetContextStatusFromResult();
            return callResponse.IsError ? null : callResponse.Result;
        }


    }
}