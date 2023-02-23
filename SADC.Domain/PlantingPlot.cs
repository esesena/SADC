using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADC.Domain
{
    public class PlantingPlot
    {
        public int PlantingId { get; set; }
        public Planting Planting { get; set; }
        public int PlotId { get; set; }
        public Plot Plot { get; set; }
    }
}
