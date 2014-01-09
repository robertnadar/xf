// <copyright file="XFConstants.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class XFConstants
    {
        public const string StrategyKey = "F30F2E17";
        public const string SystemNull = "{x:Null}";

        public static class Application
        {
            public const string UserIdentityParamName = "Username";
            public const string ApplicationKey = "xaf.application.key";
            public const string STRATEGYKEY = "xaf.strategy.key";
            public const string SYSTEMNULL = "{x:Null}";
            public const string DEFAULTAPPCONTEXTKEY = "demo";
            public const string APPUSERIDENTITYPARAMNAME = "UserIdentity";
            public const string ACTIONRESULTMODELTYPE = "ActionResult.ModelType";
            public const string ACTIONRESULT = "Action.Result";
            public const string ACTIONEXECUTESTRATEGY = "Action.ExecuteStrategy";
            public const string ACTIONEXECUTESTRATEGYDYNAMIC = "ExecuteStrategy.Dynamic";
            public const string ACTIONEXECUTESTRATEGYNORMAL = "ExecuteStrategy.Normal";
            public const string ACTIONEXECUTEMETHODNAME = "Action.MethodName";
            public const string MODELSERVICE = "model.service";

            public const string SELECTEDSTRATEGY = "selected.strategy";
            public const string PERMISSIONGROUPS = "permission.groups";
            public const string TARGETACTIONS = "target.actions";

            public static class Defaults
            {
                public const string SqlServerSchema = "dbo";
            }
        }
        public static class Category
        {
            public const string GENERAL = "General";
            public const string DEBUG = "Debug";
            public const string XF = "eXtensibleFramework";
        }

        public static class Config
        {
            
            public const string LOGGINGSTRATEGY = "app.logging.strategy";
            public const string COMMONSERVICES = "commonservices";
            public const string DEVTOOL = "devtool";
            public const string EVENTVIEWER = "eventlog";
            public const string APPUSERIDENTITYPARAMNAME = "UserIdentity";
            public const string ARCHITECTURALPLUGINS = "plugins";
            public const string COMMONSERVICESDATAFOLDERPATH = "ServicesData";
            public const string DEFAULTKEY = "none";
            public const string DEFAULTZONE = "development";
            public const string DEFAULTTIER = "none";
            public const string DEFAULTLAYER = "none";
            public const string DEFAULTCONTEXT = "demo";
            public const string DEFAULTLOGGINGSTRATEGY = "referencedassembly";
            public const string DEFAULTPUBLISHSEVERITY = "error";
            public const string DATAACCESSCONTEXTFOLDERPATH = "Data";
            public const string FRAMEWORKSTRATEGYGROUPNAME = "xF.Plugin.Strategy";
            public const string MODELS = "Models";
            public const string SECTIONNAME = "xFConfiguration";
        }

        public static class Context
        {
            public const string Application = "8B44A4E5";
            public const string DefaultApplication = "demo";
            public const string Error = "CC5D253F";
            public const string Claim = "user.claim";
            public const string LOGGINGCATEGORY = "app.logging.category";
            public const string ZONE = "app.context.zone";
            public const string TASK = "app.context.task";
            public const string MODEL = "app.context.model";
            public const string LAYER = "app.context.layer";
            public const string TIER = "app.context.tier";
            public const string MODULE = "app.context.module";
            public const string CLASS = "app.context.class";
            public const string LINE = "app.context.line";
            public const string USERIDENTITY = "app.context.user";
            public const string USERCULTURE = "app.context.culture";
            public const string UICULTURE = "app.context.uxculture";
            public const string INSTANCEIDENTIFIER = "app.context.instance";
            public const string EXECUTIONID = "app.context.executionid";
            public const string COMPONENTID = "app.context.componentid";
            public const string DATASTORE = "app.context.datastore";
            public const string ACTIVITYID = "app.context.activityid";
            public const string LOGGINGSEVERITY = "app.logging.severity";
            public const string TITLE = "XF.SOA";
            public const string SEQUENCEID = "sequenceid";
            public const string RequestBegin = "request.begin";

        }

        public static class Domain
        {
            public const string Context = "domain.context";
            public const string Claims = "domain.claims";
            public const string Metrics = "domain.metrix";
        }

        public static class Metrics
        {
            public static class Scope
            {
                public const string DataService = "layer.dataservice";
                public const string ModelService = "layer.modelservice";
                public const string DataStore = "layer.datastore";
                public const string Caching = "layer.caching";
                public const string WebApi = "layer.webapi";

                public static class ModelDataGateway
                {
                    public const string Begin = "begin.mdg";
                    public const string End = "end.mdg";
                }
                public static class SqlCommand
                {
                    public const string Begin = "begin.sqlcommand";
                    public const string End = "end.sqlcommand";
                }
                public static class Api
                {
                    public const string Begin = "begin.api";
                    public const string End = "end.api";
                }

            }
            public static class SqlServer
            {
                public const string SqlCommandText = "sql.text";
                public const string SqlCommandStoredProcedure = "sql.sproc";
                public const string Datasource = "data.source";
            }

        }
        public static class Message
        {
            public const string Verb = "B1262601";
            public const string Context = "B577435D";
            public const string RequestStatus = "912AE22D";

            public static IDictionary<string, ModelActionOption> ConstVerbList = new Dictionary<string, ModelActionOption>
            {
                {"76EA4FDD",ModelActionOption.None},
                {"D8BB96A6",ModelActionOption.Post},
                {"A2B7DD99",ModelActionOption.Put},
                {"058D2401",ModelActionOption.Delete},
                {"E68D94C5",ModelActionOption.Get},
                {"E1F58BC6",ModelActionOption.GetAll},
                {"5EA64B13",ModelActionOption.GetAllDisplay},
                {"140B79C4",ModelActionOption.ExecuteAction},
            };
            public static IDictionary<ModelActionOption,string> VerbConstList = new Dictionary<ModelActionOption,string>
            {
                {ModelActionOption.None,"76EA4FDD"},
                {ModelActionOption.Post,"D8BB96A6"},
                {ModelActionOption.Put,"A2B7DD99"},
                {ModelActionOption.Delete,"058D2401"},
                {ModelActionOption.Get,"E68D94C5"},
                {ModelActionOption.GetAll,"E1F58BC6"},
                {ModelActionOption.GetAllDisplay,"5EA64B13"},
                {ModelActionOption.ExecuteAction,"140B79C4"},
            };

            public static class Verbs
            {
                public const string None = "76EA4FDD";
                public const string Create = "D8BB96A6";
                public const string Update = "A2B7DD99";
                public const string Delete = "058D2401";
                public const string Read = "E68D94C5";
                public const string ReadList = "E1F58BC6";
                public const string ReadListDisplay = "5EA64B13";
                public const string ExecuteAction = "140B79C4";
            }
        }

        public static class ZONE
        {
            public const string LOCAL = "local";
            public const string DEVELOPMENT = "development";
            public const string QUALITYASSURANCE = "qa";
            public const string USERACCEPTANCETESTING = "uat";
            public const string STAGING = "staging";
            public const string PRODUCTION = "production";
        }
    }
}
