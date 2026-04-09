using simpli.Domain.Entities;

public interface IStatsRepo
{
 Task getTotalCheckIns(int companyID); 
  Task getDailyVolume(int companyID); 
  Task getRoomsStats(int companyID); 
  Task getRecentCkeckIns(int companyID);
  Task getAverageStayTime(int companyID);
}