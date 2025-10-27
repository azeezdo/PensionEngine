using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionSystem.Application.service
{
    public class EmployerService :IEmployerService
    {
        private readonly IUnitofWork _uow;
        public EmployerService(IUnitofWork uow)
        {
            _uow = uow;
        }

        public async Task<CustomResponse> CreateEmployerAsync(CreateEmployerDto dto)
        {
            CustomResponse res = null;
            try
            {
                var existEmployer = await _uow.Employers.GetByExpressionAsync(x => x.RegistrationNumber == dto.RegistrationNumber);
                if (existEmployer == null)
                {
                    var employer = new Employer(dto.CompanyName, dto.RegistrationNumber);
                    await _uow.Employers.AddAsync(employer);
                    await _uow.CommitAsync();
                    res = new CustomResponse(200, "employer Successfully Created", null);
                }
                res = new CustomResponse(404, $"employer with email {dto.RegistrationNumber} not found", null);

            }
            catch (Exception e)
            {

            }
            return res;
        }
        public async Task<CustomResponse> UpdateEmployerAsync(Guid id, UpdateEmployerDto dto)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.Employers.GetByIdAsync(id);
                if (employer == null)
                {
                    res = new CustomResponse(404, $"employer with id {id} not found", null);
                }
                employer.Update(dto.CompanyName, dto.RegistrationNumber);
                _uow.Employers.Update(employer);
                await _uow.CommitAsync();
                res = new CustomResponse(200, "employer Successfully Updated", null);
            }
            catch (Exception e)
            {

            }
            return res;
        }

        public async Task<CustomResponse> SoftDeleteEmployerAsync(Guid id)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.Employers.GetByIdAsync(id);
                if (employer == null)
                {
                    res = new CustomResponse(404, $"employer with id {id} not found", null);
                }
                employer.SoftDelete();
                _uow.Employers.Update(employer);
                await _uow.CommitAsync();
                res = new CustomResponse(200, "employer Successfully Deleted", null);
            }
            catch (Exception e)
            {

            }

            return res;
        }
        public async Task<CustomResponse> GetAllEmployerAsync()
        {
            CustomResponse res = null;
            IEnumerable<Employer> employers = null;
            var employerResponse = new List<GetEmployersResponse>();

            try
            {
                employers = await _uow.Employers.GetAllAsync();
                if (employers != null)
                {
                    foreach (var employer in employers)
                    {
                        employerResponse.Add(new GetEmployersResponse
                        {
                            EmployerId = employer.Id,
                            CompanyName = employer.CompanyName,
                            RegistrationNumber = employer.RegistrationNumber,
                           
                        });
                    }
                    res = new CustomResponse(200, "Member Successfully Retrieved", employerResponse.OrderBy(x => x.EmployerId));
                }
                else
                {
                    res = new CustomResponse(404, $"Members not found", employerResponse);
                }

            }
            catch (Exception e)
            {
                res = new CustomResponse(404, $"Members not found", null);
            }
            return res;
        }
        public async Task<CustomResponse> GetEmployerAsync(Guid employerId)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.Employers.GetByIdAsync(employerId);
                if (employer != null)
                {
                    var result = new GetEmployersResponse
                    {
                        EmployerId = employer.Id,
                        CompanyName = employer.CompanyName,
                        RegistrationNumber = employer.RegistrationNumber,
                      
                    };
                    res = new CustomResponse(200, "employer Successfully Retrieved", employer);
                }
                else
                {
                    res = new CustomResponse(404, $"employer not found", employer);
                }
            }
            catch (Exception e)
            {
                res = new CustomResponse(404, $"employer not found", null);
            }
            return res;
        }
    }
}
