using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class Actor
{
    public int IdActor { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Born { get; set; }

    public string? Nationality { get; set; }

    public string? ProfilePhoto { get; set; }

    public virtual ICollection<MultimediaActor> MultimediaActors { get; set; } = new List<MultimediaActor>();
}
