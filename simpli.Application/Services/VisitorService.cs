using simpli.Application.Dtos;
using simpli.Domain.Entities;

namespace simpli.Application.Services;

public class VisitorService
{
  private readonly IVisitorRepo _visitorRepo;
  private readonly VisitorMappers _mapper;

  public VisitorService(IVisitorRepo visitorRepo, VisitorMappers mapper)
  {
    _visitorRepo = visitorRepo;
    _mapper = mapper;
  }
  public async Task<VisitorDto> CheckIn(CheckInDto visitorDto, int companyID, int roomId)
  {
    var visitor = _mapper.MapToEntityFromCheckIn(visitorDto);
    await _visitorRepo.CheckIn(visitor, companyID, roomId);
    return _mapper.MapToDto(visitor);
  }
  public async Task CheckOut(CheckOutDto dto)
  {
    var entity = _mapper.MapToEntityFromCheckOut(dto);
    await _visitorRepo.CheckOut(entity);
  }
  public async Task<VisitorDto> GetVisitor(int id)
  {
    var entity = await _visitorRepo.GetVisitor(id);
    return _mapper.MapToDto(entity);
  }
  public async Task<List<VisitorDto>> GetAllVisitors(int companyID)
  {
    var visitors = await _visitorRepo.GetAllVisitors(companyID);
    return visitors.Select(v => _mapper.MapToDto(v)).ToList();
  }
}