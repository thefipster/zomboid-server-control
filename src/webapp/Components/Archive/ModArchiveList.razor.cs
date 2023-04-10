using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Archive
{
    public partial class ModArchiveList
    {
        public IList<ModArchive> Archive { get; set; } = new List<ModArchive>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Archive = ModArchive.Get().ToList();
        }
    }
}
