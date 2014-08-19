- Ajouter les sections de configuration suivantes (dans `<configSections>`): 

```XML
<section name="LogConfiguration" type="BlueInt32.Core.Logs.LogConfigurationSection,BlueInt32.Core" />
<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
```

- Ajouter la section suivante pour logger avec log4net

```XML
<LogConfiguration LogLevel="Info" LogMethods="Log4Net" />
```

- Ajouter la section log4net suivante : 

```XML
<log4net>
    <root>
            <level value="INFO" />
            <appender-ref ref="RollingFileAppender" />
    </root>
    <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
            <param name="File" value="Logs\Info.log" />
            <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
            <param name="AppendToFile" value="true" />
            <rollingStyle value="Size" />
			<encoding value="unicodeFFFE" />
            <maxSizeRollBackups value="-1" />
            <maximumFileSize value="1MB" />
            <staticLogFileName value="true" />
            <datePattern value="yyyyMMdd" />
            <layout type="log4net.Layout.PatternLayout">
                <conversionPattern value="%date %-5level %logger - %message%newline" />
            </layout>
    </appender>
</log4net>
```

- Appeler Log.Init() dans le Application_Start

- Loger avec Log.Info("source", "texte à logger");
