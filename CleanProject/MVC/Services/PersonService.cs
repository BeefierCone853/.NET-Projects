using AutoMapper;
using MVC.Abstractions;
using MVC.Models;
using MVC.Services.Base;

namespace MVC.Services;

public class PersonService(
    IClient httpClient,
    ILocalStorageService localStorage,
    IMapper mapper) : BaseHttpService(httpClient, localStorage), IPersonService
{
    public async Task<List<PersonViewModel>> GetPersons()
    {
        var persons = await _client.PersonsAllAsync();
        return mapper.Map<List<PersonViewModel>>(persons);
    }

    public async Task<PersonViewModel> GetPersonDetails(int id)
    {
        var person = await _client.PersonsGETAsync(id);
        return mapper.Map<PersonViewModel>(person);
    }

    public async Task<Response<int>> CreatePerson(PersonViewModel person)
    {
        try
        {
            var response = new Response<int>();
            var createPerson = mapper.Map<CreatePersonDto>(person);
            var apiResponse = await _client.PersonsPOSTAsync(createPerson);
            if (apiResponse.IsSuccess)
            {
                // response.Data = apiResponse.Id;
                response.Success = true;
            }
            else
            {
                // foreach loop
                response.ValidationErrors += apiResponse.Error;
            }

            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<int>> UpdatePerson(int id, PersonViewModel person)
    {
        try
        {
            var personDto = mapper.Map<UpdatePersonDto>(person);
            await _client.PersonsPUTAsync(id, personDto);
            return new Response<int>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<int>> DeletePerson(int id)
    {
        try
        {
            await _client.PersonsDELETEAsync(id);
            return new Response<int>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }
}