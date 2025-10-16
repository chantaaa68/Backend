using Backend.Annotation;
using Backend.Dto.service.icon;
using Backend.Repository;

namespace Backend.service
{
    [Component]
    public class IconService(IconRepository _repository)
    {
        public readonly IconRepository repository = _repository;

        public async Task<GetIconListResponse> GetIconListAsync()
        {
            GetIconListResponse response = new()
            {
                IconDatas = (await this.repository.GetIconListAsync()).Select(i => new IconData
                {
                    IconId = i.Id,
                    OfficialIconName = i.OfficialIconName,
                    DefaultIconName = i.DefaultIconName
                }).ToList()
            };

            return response;
        }
    }
}
