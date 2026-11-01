namespace EaFramework.Config;

public class TestSettings
{
    public string[] Args { get; set; } = Array.Empty<string>();
    
    public float Timeout { get; set; } = 30.0f;
    public bool Headless { get; set; } = false;

    public int SlowMo { get; set; } = 0;

    public DriverType DriverType { get; set; }
    
    public string ApplicationUrl { get; set; } = string.Empty;
}

public enum DriverType
{
    Chromium,
    Firefox,
    Edge,
    Webkit 
}