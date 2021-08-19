using PluginsAPI;

namespace ClearCore.Optimize
{
    public class OptimizeSettings
    {
        public string Type;
        public Plugin Script;

        public OptimizeSettings(string name, Plugin script)
        {
            this.Type = name;
            this.Script = script;
        }
    }
}
