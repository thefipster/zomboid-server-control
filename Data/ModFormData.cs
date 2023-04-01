﻿namespace TheFipster.Zomboid.ServerControl.Data
{
    public class ModFormData
    {
        public ModFormData() => Reset();

        public string? ModId { get; set; }
        public string? ModName { get; set; }
        public string? WorkshopId { get; set; }

        public void Reset()
        {
            ModId = string.Empty; 
            ModName = string.Empty;
            WorkshopId = string.Empty;
        }

        public bool Valid => 
            !string.IsNullOrWhiteSpace(ModId) 
            && !string.IsNullOrWhiteSpace(ModName) 
            && !string.IsNullOrWhiteSpace(WorkshopId);
    }
}
