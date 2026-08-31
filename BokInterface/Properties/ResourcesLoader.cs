using System.Resources;

namespace BokInterface.Properties;

public class ResourceLoader {
    public static object LoadResource(string resourceName, string resourceKey) {
        try {
            ResourceManager resourceManager = new("BokInterface.Properties." + resourceName, typeof(ResourceLoader).Assembly);
            return resourceManager.GetObject(resourceKey);
        } catch {
            return "";
        }
    }
}
