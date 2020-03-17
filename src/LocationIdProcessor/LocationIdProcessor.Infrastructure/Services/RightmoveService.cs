﻿using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RightmovePostcodeToLocationId.LocationIdProcessor.Core.Interfaces.Infrastructure;

namespace RightmovePostcodeToLocationId.LocationIdProcessor.Infrastructure.Services
{
    public class RightmoveService : IRightmoveService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RightmoveService> _rightmoveServiceLogger;

        public RightmoveService(HttpClient httpClient, ILogger<RightmoveService> rightmoveServiceLogger)
        {
            _httpClient = httpClient;
            _rightmoveServiceLogger = rightmoveServiceLogger;
        }

        public async Task GetDataAsync(string postcode, string locationId)
        {
            var uri =  $"https://www.rightmove.co.uk/api/_search?locationIdentifier=POSTCODE%5{locationId}&numberOfPropertiesPerPage=24&radius=0.0&sortType=6&index=0&includeLetAgreed=false&viewType=LIST&channel=RENT&areaSizeUnit=sqft&currencyCode=GBP&isFetching=false&viewport=";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            // required headers for Rightmove
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36");

            var response = await _httpClient.SendAsync(request);
            var res = await response.Content.ReadAsStringAsync();
            //TODO do something with the results
            _rightmoveServiceLogger.LogInformation($"Processing postcode {postcode} and locationId {locationId}");
        }
    }
}
