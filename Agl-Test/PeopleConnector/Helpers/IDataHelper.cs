namespace Agl.Connectors.Helpers
{
    public interface IDataHelper
    {
        T DeserializeData<T>(string jsonData);
        string GetJsonData(string serviceUrl);
    }
}