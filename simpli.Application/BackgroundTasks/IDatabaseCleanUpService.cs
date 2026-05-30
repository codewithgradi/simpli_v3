namespace simpli.Application.BackgroundTasks;

public interface IDatabaseCleanUpService
{
  Task CleanOldDataAsync(CancellationToken cancellationToken);
}