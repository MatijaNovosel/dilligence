﻿using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class Odjel
  {
    public Odjel()
    {
      Zaposlenik = new HashSet<Zaposlenik>();
    }

    public int Id { get; set; }
    public string Naziv { get; set; }

    public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
  }
}