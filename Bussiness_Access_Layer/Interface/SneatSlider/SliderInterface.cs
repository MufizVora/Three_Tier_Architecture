using Data_Access_Layer.Models.SneatDashboard;
using DTOLayer.DTOs_Models.Product_DTOs;
using DTOLayer.DTOs_Models.Slider_DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Access_Layer.Interface.SneatSlider
{
    public interface SliderInterface
    {
        Task<string> CreateSlider(SliderDTO slider, IFormFile file, IFormFile[] MultiImage);

        public List<Slider> GetSliders(string AdminId);

    }
}
