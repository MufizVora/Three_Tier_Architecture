using AutoMapper;
using Bussiness_Access_Layer.Interface.SneatSlider;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.SneatDashboard;
using DTOLayer.DTOs_Models.Slider_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Access_Layer.Service.SneatSlider
{
    public class SliderService : SliderInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly SliderFileUpload _file;

        public SliderService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, SliderFileUpload file)
        {
            _context = context;
            _mapper = mapper;
            _file = file;
        }
        public async Task<string> CreateSlider(SliderDTO slider, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            try
            {
                int count = 0;
                foreach (var item in MultiImage)
                {
                    if (count == 0)
                    {
                        var path = await _file.UploadProductFile(item);
                        slider.File += path;
                    }
                    else
                    {
                        var filepath = await _file.UploadProductFile(item);
                        slider.MultiImage += filepath + ",";
                    }
                    count++;
                }

                var SlideEntity = _mapper.Map<Slider>(slider);
                _context.Sliders.Add(SlideEntity);
                _context.SaveChanges();

                response = "Success";
                return response;
            }
            catch
            {
                response = "Failed";
                return response;
            }

        }

        public List<Slider> GetSliders(string AdminId)
        {
            var data = _context.Sliders.Where(a => a.AdminId == AdminId).ToList();
            return data;
        }
    }
}
