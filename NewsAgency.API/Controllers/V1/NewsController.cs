using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NewsAgency.Services;
using NewsAgency.Services.Abstraction;
using NewsAgency.DAL.Entities;
using System.Threading.Tasks;
using NewsAgency.API.Contracts.V1;
using static NewsAgency.API.Extensions.HttpContextExtensions;
using static System.IO.Path;
using NewsAgency.API.Contracts.V1.Requests;
using AutoMapper;

namespace NewsAgency.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService<News, NewsCategory> _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService<News, NewsCategory> newsService, 
                              IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpGet]
        public  IActionResult Get() =>  new OkObjectResult(_newsService.GetNewsList().ToArray());
        
        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetCount() => new OkObjectResult(await _newsService.GetCountAsync());


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateNewsRequest newsRequest)
        {
            News news =  _mapper.Map<News>(newsRequest);
            await _newsService.AddNewsAsync(news);
            //return Ok(news);
            string locationUrl = Combine(HttpContext.GetBaseUrl(), ApiRoutes.News.Get.Replace("{newsId}", news.Id.ToString()));
            return Created(locationUrl, news);
        }

    }
}
