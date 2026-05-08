using simpli.Domain;
using simpli.Domain.Entities;

public interface IVisitorRepo
{
    Task CheckIn(CheckInDto dto, int companyId, int roomId);
    Task CheckOut(CheckOutDto dto);
    Task<VisitorDto> GetVisitor(int id);
    Task<List<CheckInDto>> GetAllVisitors(int companyID);
}