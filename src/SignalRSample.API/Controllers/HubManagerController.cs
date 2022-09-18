using Microsoft.AspNetCore.Mvc;
using SignalR;
using SignalR.Contract;
using SignalR.Domain;

namespace SignalRSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubManagerController : ControllerBase
    {
        private readonly IHubNotifictionContext _hubNotifictionContext;
        public HubManagerController(IHubNotifictionContext hubNotifictionContext)
        {
            _hubNotifictionContext = hubNotifictionContext;
        }

        [HttpPost("GellAllUsers")]
        public ActionResult<GetUserListQueryResult> GellAllUsers()
        {
            var userList = _hubNotifictionContext.GetAllHubUsers();
                
            var userEntities = new List<GetUserListQueryModel>();
            userEntities.AddRange(userList.Select(x => new GetUserListQueryModel
            {
                ConnectDateTime = x.ConnectDateTime,
                ConnectionId = x.ConnectionId,
                UserId=x.UserId,
            }));

            var resultModel = new GetUserListQueryResult();
            resultModel.Entities = userEntities;
            resultModel.Success = true;
            return Ok(resultModel);
        }

        [HttpPost("NotifyToastToUser")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyToastToUser(NotifyToastUserDto notifyToastUserDto)
        {
            var model = new ToastNotification
            {
                Message = notifyToastUserDto.Message,
                NotificationTypeAlias = notifyToastUserDto.NotificationTypeAlias,
                Title = notifyToastUserDto.Title,
                Url = notifyToastUserDto.Url
            };

            await _hubNotifictionContext.NotifyToastToUserAsync(notifyToastUserDto.UserId, model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }

        [HttpPost("NotifyToastToUsers")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyToastToUsers(NotifyToastUsersDto notifyToastUsersDto)
        {
            var model = new ToastNotification
            {
                Message = notifyToastUsersDto.Message,
                NotificationTypeAlias = notifyToastUsersDto.NotificationTypeAlias,
                Title = notifyToastUsersDto.Title,
                Url = notifyToastUsersDto.Url
            };

            await _hubNotifictionContext.NotifyToastToUsersAsync(notifyToastUsersDto.UserIds, model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }

        [HttpPost("NotifyToastToAllUsers")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyToastToAllUsers(NotifyToastAllUsersDto notifyToastAllUsersDto)
        {
            var model = new ToastNotification
            {
                Message = notifyToastAllUsersDto.Message,
                NotificationTypeAlias = notifyToastAllUsersDto.NotificationTypeAlias,
                Title = notifyToastAllUsersDto.Title,
                Url = notifyToastAllUsersDto.Url
            };
            await _hubNotifictionContext.NotifyToastToAllUsersAsync(model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }

        [HttpPost("NotifyEventToUser")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyEventToUser(NotifyEventUserDto notifyEventUserDto)
        {
            var model = new EventNotification
            {
                Url = notifyEventUserDto.Url,
                EventTypeAlias = notifyEventUserDto.EventTypeAlias
            };

            await _hubNotifictionContext.NotifyEventToUserAsync(notifyEventUserDto.UserId, model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }

        [HttpPost("NotifyEventToUsers")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyEventToUser(NotifyEventUsersDto notifyEventUsersDto)
        {
            var model = new EventNotification
            {
                Url = notifyEventUsersDto.Url,
                EventTypeAlias = notifyEventUsersDto.EventTypeAlias
            };

            await _hubNotifictionContext.NotifyEventToUsersAsync(notifyEventUsersDto.UserIds, model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }

        [HttpPost("NotifyEventToAllUsers")]
        public async Task<ActionResult<NotifyUserCommandResult>> NotifyEventToAllUsers(NotifyEventAllUsersDto notifyEventAllUsersDto)
        {
            var model = new EventNotification
            {
                Url = notifyEventAllUsersDto.Url,
                EventTypeAlias = notifyEventAllUsersDto.EventTypeAlias
            };

            await _hubNotifictionContext.NotifyEventToAllUsersAsync(model);

            var okResult = new NotifyUserCommandResult();
            okResult.Success = true;
            return Ok(okResult);
        }
    }
}
