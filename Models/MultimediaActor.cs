using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class MultimediaActor
{
    public int IdMultimediaActor { get; set; }

    public int IdActor { get; set; }

    public int? IdMovie { get; set; }

    public int? IdSerie { get; set; }

    public int? IdSport { get; set; }

    public virtual Actor IdActorNavigation { get; set; } = null!;

    public virtual Movie? IdMovieNavigation { get; set; }

    public virtual Series? IdSerieNavigation { get; set; }

    public virtual Sport? IdSportNavigation { get; set; }
}
