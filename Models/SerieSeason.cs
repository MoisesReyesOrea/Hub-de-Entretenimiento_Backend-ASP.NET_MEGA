using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class SerieSeason
{
    public int IdSeason { get; set; }

    public int IdSerie { get; set; }

    public string Title { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CoverImg { get; set; }

    public virtual Series IdSerieNavigation { get; set; } = null!;

    public virtual ICollection<SerieEpisode> SerieEpisodes { get; set; } = new List<SerieEpisode>();
}
