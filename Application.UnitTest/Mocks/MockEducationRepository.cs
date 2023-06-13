namespace Application.UnitTest.Mocks;

public class MockEducationRepository
{   public static Mock<IEducationRepository> GetEducationRepository()
    {
        var educations = new List<Education>
        {
           new Education
                {},
           new Education
                {}
        };

        var mockRepository = new Mock<IEducationRepository>();

        mockRepository.Setup(r => r.GetAll()).ReturnsAsync(chores);

        mockRepository.Setup(r => r.Add(It.IsAny<Education>())).ReturnsAsync((Education chore) =>
        {
            chore.Id = chores.Count() + 1;
            chores.Add(chore);
            return chore;
        });

        mockRepository.Setup(r => r.Update(It.IsAny<Education>())).Callback((Education chore) =>
        {
            var newChores = chores.Where((r) => r.Id != chore.Id);
            chores = newChores.ToList();
            chores.Add(chore);
        });

        mockRepository.Setup(r => r.Delete(It.IsAny<Education>())).Callback((Education chore) =>
        {
            if (chores.Exists(b => b.Id == chore.Id))
                chores.Remove(chores.Find(b => b.Id == chore.Id)!);
        });

        mockRepository.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var chore = chores.FirstOrDefault((r) => r.Id == id);
            return chore != null;
        });

        mockRepository.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return chores.FirstOrDefault((r) => r.Id == id);
        });

        return mockRepository;
    }
}