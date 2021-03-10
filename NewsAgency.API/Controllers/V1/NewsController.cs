using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using NewsAgency.Services.Abstraction;
using NewsAgency.DAL.Entities;
using System.Threading.Tasks;
using NewsAgency.API.Contracts.V1;
using static NewsAgency.API.Extensions.HttpContextExtensions;
using NewsAgency.API.Contracts.V1.Requests;
using AutoMapper;
using NewsAgency.API.Attributes;
using System.IO;
using static NewsAgency.API.Helpers.Helpers;
using NewsAgency.API.Contracts.V1.Responses;
using System.Collections.Generic;
using Hangfire;

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
        //[Cached(3600)]
        public  IActionResult Get() =>  new OkObjectResult(_newsService.GetNewsList().ToArray());

        [HttpGet]
        //[Cached(3600)]
        [Route("Download")]
        public async Task<FileContentResult> DownloadAsTxt()
        {
            var news = _mapper.Map<IEnumerable<DownloadNewsResponse>>(_newsService.GetNewsList());
            string data = string.Join("", news);
            var (fileContent, fileName) = await SaveAsAsync(data);
            return File(fileContent, "application/octet-stream", fileName);
        }

        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetCount() => new OkObjectResult(await _newsService.GetCountAsync());


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateNewsRequest newsRequest)
        {
            News news =  _mapper.Map<News>(newsRequest);
            await _newsService.AddNewsAsync(news);
            //return Ok(news);
            string locationUrl = Path.Combine(HttpContext.GetBaseUrl(), ApiRoutes.News.Get.Replace("{newsId}", news.Id.ToString()));
            return Created(locationUrl, news);
        }

    }
}
