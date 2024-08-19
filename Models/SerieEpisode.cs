using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class SerieEpisode
{
    public int IdEpisode { get; set; }

    public int IdSeason { get; set; }

    public string Title { get; set; } = null!;

    public short EpisodeNumber { get; set; }

    public string Date { get; set; } = null!;

    public string? Duration { get; set; }

    public string Description { get; set; } = null!;

    public string? CoverImg { get; set; }

    public string? VideoUrl { get; set; }

    public virtual SerieSeason IdSeasonNavigation { get; set; } = null!;
}
