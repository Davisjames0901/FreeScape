using System.Threading.Tasks;

namespace FreeScape.Engine.Actions
{
    public class MappedAction
    {
        public string Action { get; set; }
        public string Button { get; set; }
        public string Device { get; set; }
        public bool OnPressed { get; set; }
        public bool OnReleased { get; set; }
    }
}