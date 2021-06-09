using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class CourseService : ICourseService
    {
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _http;

        public CourseService(IConfiguration confiq, HttpClient http)
        {
            _http = http;
            _baseUrl = confiq.GetSection("api:baseUrl").Value;

            _options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<bool> AddCourse(CourseModel model)
        {

            try
            {
                var url = _baseUrl + "courses";
                var data = JsonSerializer.Serialize(model);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if(response.IsSuccessStatusCode){
                    return true;
                } else {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                
            }
        }


        public async Task<CourseModel> GetCourseAsync(int id)
        {
            var response = await _http.GetAsync($"{_baseUrl}courses/{id}");


            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<CourseModel> GetCourseByNoAsync(int courseNo)
        {
            var response = await _http.GetAsync($"{_baseUrl}courses/find/{courseNo}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            var response = await _http.GetAsync($"{_baseUrl}courses");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<bool> UpdateCourse(int id, UpdateCourseViewModel model)
        {
            try
            {
                var url = $"{_baseUrl}courses/{id}";
                var data = JsonSerializer.Serialize(model);
                var response = await _http.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                return true;
                }
                else
                {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCourse(int courseNo)
        {
            try
            {
                var url = $"{_baseUrl}{courseNo}";
                var response = await _http.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                return true;
                }
                else
                {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Participant

        public async Task<List<ParticipantModel>> GetCParticipantsAsync()
        {

            var response = await _http.GetAsync($"{_baseUrl}participants");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ParticipantModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<ParticipantModel> GetParticipantAsync(int id)
        {
            var response = await _http.GetAsync($"{_baseUrl}participants/{id}");


            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ParticipantModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<ParticipantModel> GetParticipantByEmailAsync(string email)
        {
            var response = await _http.GetAsync($"{_baseUrl}participants/find/{email}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ParticipantModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<bool> AddParticipant(ParticipantModel model)
        {
            
            try
            {
                var url = _baseUrl + "participants";
                var data = JsonSerializer.Serialize(model);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if(response.IsSuccessStatusCode){
                    return true;
                } else {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                
            }
        }

        public async Task<bool> UpdateParticipant(int id, UpdateParticipantViewModel model)
        {
            try
            {
                var url = $"{_baseUrl}participants/{id}";
                var data = JsonSerializer.Serialize(model);
                var response = await _http.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                return true;
                }
                else
                {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteParticipant(string email)
        {
            try
            {
                var url = $"{_baseUrl}{email}";
                var response = await _http.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                return true;
                }
                else
                {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}