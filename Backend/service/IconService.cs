using Backend.Annotation;
using Backend.Dto.service.icon;
using Backend.Repository;
using Backend.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Backend.service
{
    [AutoDI]
    public class IconService(IconRepository _repository)
    {
        public readonly IconRepository repository = _repository;

        public async Task<IActionResult> GetIconListAsync()
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

            return ApiResponseHelper.Success(response);
        }
    }
}
