using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class Director
{
    public int IdDirector { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Born { get; set; }

    public string? Nationality { get; set; }

    public string? ProfilePhoto { get; set; }

    public virtual ICollection<MultimediaDirector> MultimediaDirectors { get; set; } = new List<MultimediaDirector>();
}
