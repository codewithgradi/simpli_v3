using simpli.Domain;
using simpli.Domain.Entities;

public interface IVisitorRepo
{
    Task CheckIn(Visitor visitor, int companyId, int roomId);
    Task CheckOut(Visitor visitor);
    Task<VisitorDto> GetVisitor(int id);
    Task<List<CheckInDto>> GetAllVisitors(int companyID);
}