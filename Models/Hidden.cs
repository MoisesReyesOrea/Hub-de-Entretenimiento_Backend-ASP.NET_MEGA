using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class Hidden
{
    public int IdHidden { get; set; }

    public int IdUser { get; set; }

    public int? IdMovie { get; set; }

    public int? IdSerie { get; set; }

    public int? IdSport { get; set; }

    public byte[] CreatedAt { get; set; } = null!;

    public virtual Movie? IdMovieNavigation { get; set; }

    public virtual Series? IdSerieNavigation { get; set; }

    public virtual Sport? IdSportNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
