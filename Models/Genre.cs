using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class Genre
{
    public int IdGenre { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<MultimediaGenre> MultimediaGenres { get; set; } = new List<MultimediaGenre>();
}
