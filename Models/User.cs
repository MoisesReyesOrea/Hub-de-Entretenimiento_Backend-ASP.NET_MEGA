using System;
using System.Collections.Generic;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public short? Age { get; set; }

    public string? ProfilePhoto { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Hidden> Hiddens { get; set; } = new List<Hidden>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
