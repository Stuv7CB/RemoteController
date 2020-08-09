using AutoMapper;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;

namespace RemoteController.Server.Managers.Xbox
{
    public class XboxProfile : Profile
    {
        private Xbox360Button Convert(Rpc.XboxMessageRequest.Types.Buttons button) => button switch
        {
            Rpc.XboxMessageRequest.Types.Buttons.A => Xbox360Button.A,
            Rpc.XboxMessageRequest.Types.Buttons.Up => Xbox360Button.Up,
            Rpc.XboxMessageRequest.Types.Buttons.Y => Xbox360Button.Y,
            Rpc.XboxMessageRequest.Types.Buttons.X => Xbox360Button.X,
            Rpc.XboxMessageRequest.Types.Buttons.B => Xbox360Button.B,
            Rpc.XboxMessageRequest.Types.Buttons.Guide => Xbox360Button.Guide,
            Rpc.XboxMessageRequest.Types.Buttons.RightShoulder => Xbox360Button.RightShoulder,
            Rpc.XboxMessageRequest.Types.Buttons.LeftShoulder => Xbox360Button.LeftShoulder,
            Rpc.XboxMessageRequest.Types.Buttons.LeftThumb => Xbox360Button.LeftThumb,
            Rpc.XboxMessageRequest.Types.Buttons.Back => Xbox360Button.Back,
            Rpc.XboxMessageRequest.Types.Buttons.Start => Xbox360Button.Start,
            Rpc.XboxMessageRequest.Types.Buttons.Right => Xbox360Button.Right,
            Rpc.XboxMessageRequest.Types.Buttons.Left => Xbox360Button.Left,
            Rpc.XboxMessageRequest.Types.Buttons.Down => Xbox360Button.Down,
            Rpc.XboxMessageRequest.Types.Buttons.RightThumb => Xbox360Button.RightThumb,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public XboxProfile()
        {
            CreateMap<Rpc.XboxMessageRequest.Types.Buttons, Xbox360Button>()
                .ConvertUsing(b => Convert(b));
        }
    }
}
