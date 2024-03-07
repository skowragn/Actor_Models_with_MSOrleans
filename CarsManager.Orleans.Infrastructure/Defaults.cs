namespace Orleans.Hosting;

public static class Defaults
{
    public static string ClusterName => "Cluster";
    public static string ClusterId => "CarReservationCluster";
    public static string ServiceName => "Service";
    public static string SiloName => "Silo";
    public static string InstanceId => "InstanceId";
    public static string CarReservations => "car-reservations";
    public static int SiloPort => 11111;
    public static int GatewayPort => 30000;
}

public static class EnvironmentVariables
{
    public static string ApplicationInsightsInstrumentationKey => "APPINSIGHTS_INSTRUMENTATIONKEY";
    public static string OrleansClusterName => "ORLEANS_CLUSTER_ID";
    public static string OrleansServiceName => "ORLEANS_SERVICE_ID";
    public static string OrleansSiloPort => "ORLEANS_SILO_PORT";
    public static string OrleansPrimarySiloPort => "ORLEANS_PRIMARY_SILO_PORT";
    public static string OrleansSiloName => "ORLEANS_SILO_NAME";
    public static string OrleansWebName => "ORLEANS_WEB_NAME";
    public static string OrleansGatewayPort => "ORLEANS_GATEWAY_PORT";
    public static string AzureStorageConnectionString => "ORLEANS_AZURE_STORAGE_CONNECTION_STRING";
    public static string WebAppsPrivateIpAddress => "WEBSITE_PRIVATE_IP";
    public static string WebAppsPrivatePorts => "WEBSITE_PRIVATE_PORTS";
    public static string KubernetesPodName => "POD_NAME";
    public static string KubernetesPodNamespace => "POD_NAMESPACE";
    public static string KubernetesPodIpAddress => "POD_IP";
}