using System.Collections.Generic;
using TechTalk.SpecFlow;
using System;
using Plexure.Service.Security.Sdk;
using Plexure.Service.Security.Sdk.Models;

namespace Plexure.Service.Security.IntegrationTest
{
    [Binding]
    public sealed class BeforeAfterScenario
    {
        [BeforeScenario]
        public static void BeforeScenario()
        {
            SuffixGen.ReadRequestSuffix();
            Environment.RequestSuffix = SuffixGen.Suffix;

            List<Guid?> accountIdList = new List<Guid?>();
            ScenarioContext.Current["accountIdList"] = accountIdList;

            List<Guid?> tenantIdList = new List<Guid?>();
            ScenarioContext.Current["tenantIdList"] = tenantIdList;
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            SuffixGen.WriteRequestSuffix();

            List<Guid?> tenantIdList = ScenarioContext.Current.Get<List<Guid?>>("tenantIdList");
            DeleteTenantSteps deleteTenant = new DeleteTenantSteps();
            foreach (Guid? Id in tenantIdList)
            {
                Guid tenantId = Id.Value;
                deleteTenant.DeleteTenant(tenantId);
            }

            List<Guid?> accountIdList = ScenarioContext.Current.Get<List<Guid?>>("accountIdList");

            DeleteAccountSteps deleteAccount = new DeleteAccountSteps();
            foreach (Guid? Id in accountIdList)
            {
                Guid accountId = Id.Value;
                //delete the account's tenant
                GetAccountSteps getAnAccount = new GetAccountSteps();
                ApiError apiError;
                Account account =  getAnAccount.getAccount(accountId, out apiError);
                foreach (AccountTenant info in account.Tenants)
                {
                    deleteTenant.DeleteTenant(info.TenantId.Value);
                }
                deleteAccount.deleteAccount(accountId);
             }
           }


        }
}