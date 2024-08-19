using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class MultimediaDirector
{
    public int IdMultimediaDirector { get; set; }

    public int IdDirector { get; set; }

    public int? IdMovie { get; set; }

    public int? IdSerie { get; set; }

    public int? IdSport { get; set; }

    public virtual Director IdDirectorNavigation { get; set; } = null!;

    public virtual Movie? IdMovieNavigation { get; set; }

    public virtual Series? IdSerieNavigation { get; set; }

    public virtual Sport? IdSportNavigation { get; set; }
}
