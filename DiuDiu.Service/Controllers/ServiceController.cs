﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using DiuDiu.Data;
using DiuDiu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiuDiu
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private IMapper _mapper;
        private IHttpClientFactory _httpClientFactory;

        public ServiceController(ILogger<ServiceController> logger, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public List<ServiceItemDto> Get()
        {
            return _mapper.Map<List<ServiceItemDto>>(DataStore.Services);
        }

        [HttpPost]
        public bool Post(ServiceEditDto serviceDto)
        {
            if (string.IsNullOrWhiteSpace(serviceDto.Name))
            {
                return false;
            }
            if (DataStore.Services.Any(a => a.ID == serviceDto.ID))
            {
                return false;
            }
            var gateway = DataStore.Gateways.Where(a => a.ServiceName == serviceDto.Name && a.ServiceSecret == serviceDto.Secret).FirstOrDefault();
            if (gateway == null)
            {
                return false;
            }

            var service = _mapper.Map<Service>(serviceDto);

            DataStore.Services.Add(service);

            if (service.Check != null)
            {
                // TODO  添加健康检查定时
                var task = Task.Run(() =>
                {
                    CheckTimer timer = new CheckTimer();
                    timer.Interval = service.Check.Interval * 1000;
                    timer.Elapsed += TimerCallBack;
                    timer.AutoReset = true;
                    timer.Enabled = true;
                    timer.Key = service.ID;
                    timer.Start();
                    //timer.
                });
                service.ChckTask = task;
            }

            return true;
        }

        private async void TimerCallBack(object sender, ElapsedEventArgs e)
        {
            CheckTimer timer = (CheckTimer)sender;
            var service = DataStore.Services.FirstOrDefault(a => a.ID == timer.Key);
            if (service == null)
            {
                return;
            }
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = new TimeSpan(0, 0, service.Check.TimeOut);

            Console.WriteLine($"TimerCallBack:{service.Host}:{service.Port}");
            try
            {
                var resp = await httpClient.GetAsync(service.Check.Address);
                if (resp.IsSuccessStatusCode)
                {
                    service.Error = 0;
                    service.CheckOK = true;
                }
                else
                {
                    service.Error++;
                }
            }
            catch (Exception ex)
            {
                //
                service.Error++;
            }

            if (service.Error > service.Check.ErrorTimes)
            {
                DataStore.Services.Remove(service);
            }
        }
    }

    public class CheckTimer : Timer
    {
        public string Key { get; set; }
    }
}