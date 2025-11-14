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
                var existEmployer = await _uow.employerRepo.GetByExpressionAsync(x => x.RegistrationNumber == dto.RegistrationNumber);
                if (existEmployer == null)
                {
                    var employer = new Employer(dto.CompanyName, dto.RegistrationNumber);
                    await _uow.employerRepo.AddAsync(employer);
                    await _uow.CompleteAsync();
                    res = new CustomResponse(200, "employer Successfully Created", null);
                }
                else
                {
                    res = new CustomResponse(409, $"employer with email {dto.RegistrationNumber} already exist", null);
                }
            }
            catch (Exception ex)
            {
                res = new CustomResponse(500, ex.Message);
            }
            return res;
        }
        public async Task<CustomResponse> UpdateEmployerAsync(Guid id, UpdateEmployerDto dto)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.employerRepo.GetByIdAsync(id);
                if (employer == null)
                {
                    res = new CustomResponse(404, $"employer with id {id} not found", null);
                }
                employer.Update(dto.CompanyName, dto.RegistrationNumber);
                _uow.employerRepo.Update(employer);
                await _uow.CompleteAsync();
                res = new CustomResponse(200, "employer Successfully Updated", null);
            }
            catch (Exception ex)
            {
                res = new CustomResponse(500, ex.Message);
            }
            return res;
        }

        public async Task<CustomResponse> SoftDeleteEmployerAsync(Guid id)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.employerRepo.GetByIdAsync(id);
                if (employer == null)
                {
                    res = new CustomResponse(404, $"employer with id {id} not found", null);
                }
                employer.SoftDelete();
                _uow.employerRepo.Update(employer);
                await _uow.CompleteAsync();
                res = new CustomResponse(200, "employer Successfully Deleted", null);
            }
            catch (Exception ex)
            {
                res = new CustomResponse(500, ex.Message);
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
                employers = await _uow.employerRepo.GetAllAsync();
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
            catch (Exception ex)
            {
                res = new CustomResponse(500, ex.Message);
            }
            return res;
        }
        public async Task<CustomResponse> GetEmployerAsync(Guid employerId)
        {
            CustomResponse res = null;
            try
            {
                var employer = await _uow.employerRepo.GetByIdAsync(employerId);
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
            catch (Exception ex)
            {
                res = new CustomResponse(500, ex.Message);
            }
            return res;
        }
    }
}
