using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class Series
{
    public int IdSerie { get; set; }

    public string Title { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CoverImg { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Hidden> Hiddens { get; set; } = new List<Hidden>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<MultimediaActor> MultimediaActors { get; set; } = new List<MultimediaActor>();

    public virtual ICollection<MultimediaDirector> MultimediaDirectors { get; set; } = new List<MultimediaDirector>();

    public virtual ICollection<MultimediaGenre> MultimediaGenres { get; set; } = new List<MultimediaGenre>();

    public virtual ICollection<SerieSeason> SerieSeasons { get; set; } = new List<SerieSeason>();
}
