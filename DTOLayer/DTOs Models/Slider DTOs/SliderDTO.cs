﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs_Models.Slider_DTOs
{
    public class SliderDTO
    {
        public int Id { get; set; }

        public string? AdminId { get; set; }

        public string? Title { get; set; }

        public string? File { get; set; }

        public string? MultiImage { get; set; }
    }
}
