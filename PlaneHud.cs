using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PlaneHud
{
	public class PlaneHud : BaseScript
	{
        public PlaneHud()
        {
            Tick += OnTick;
        }

        private void DrwTxt(float x, float y, float width, float height, float scale, string caption, int r, int g, int b, int a, int font, int justify, bool shadow, bool outline, int wordWrap)
		{
			API.SetTextFont(font);
			API.SetTextScale(scale, scale);
			API.SetTextColour(r, g, b, a);
			if (shadow)
			{
				API.SetTextDropShadow();
			}
			if (outline)
			{
				API.SetTextOutline();
			}
			API.SetTextEntry("STRING");
			API.AddTextComponentString(caption);
			API.DrawText(x - width / 2f, y - height / 2f + 0.005f);
		}

		private async Task OnTick()
		{
			int num = API.PlayerPedId();
			API.GetEntityCoords(num, false);
			int vehiclePedIsIn = API.GetVehiclePedIsIn(num, false);
			if (API.IsPedInAnyPlane(num) || API.IsPedInAnyHeli(num))
			{
				float entityHeightAboveGround = API.GetEntityHeightAboveGround(vehiclePedIsIn);
				float entityRoll = API.GetEntityRoll(vehiclePedIsIn);
				float entityPitch = API.GetEntityPitch(vehiclePedIsIn);
				float entityHeading = API.GetEntityHeading(vehiclePedIsIn);
				float z = API.GetEntitySpeedVector(vehiclePedIsIn, true).Z * 196.85f;
				double speed = (double)Game.PlayerPed.CurrentVehicle.Speed * 2.24;
				entityHeightAboveGround = (float)((int)Math.Round((double)entityHeightAboveGround, 0));
				entityRoll = (float)((int)Math.Round((double)entityRoll, 0));
				entityPitch = (float)((int)Math.Round((double)entityPitch, 0));
				entityHeading = (float)((int)Math.Round((double)entityHeading, 0));
				z = (float)((int)Math.Round((double)z, 0));
				speed = (double)((int)Math.Round(speed, 0));
				this.DrwTxt(0.516f, 1.246f, 1f, 1f, 0.39f, string.Format("~y~Height: ~c~{0} FT", entityHeightAboveGround), 255, 255, 255, 255, 4, 0, true, true, 0);
				this.DrwTxt(0.567f, 1.246f, 1f, 1f, 0.39f, string.Format("~y~Roll: ~c~{0}", entityRoll), 255, 255, 255, 255, 4, 0, true, true, 0);
				this.DrwTxt(0.602f, 1.246f, 1f, 1f, 0.39f, string.Format("~y~Pitch: ~c~{0}", entityPitch), 255, 255, 255, 255, 4, 0, true, true, 0);
				this.DrwTxt(0.516f, 1.269f, 1f, 1f, 0.39f, string.Format("~y~Heading: ~c~{0}", entityHeading), 255, 255, 255, 255, 4, 0, true, true, 0);
				this.DrwTxt(0.563f, 1.269f, 1f, 1f, 0.39f, string.Format("~y~Vertical Speed: ~c~{0} Ft/Min", z), 255, 255, 255, 255, 4, 0, true, true, 0);
				this.DrwTxt(0.594f, 1.435f, 1f, 1f, 0.39f, string.Format("~y~Speed: ~c~{0} Knots", speed), 255, 255, 255, 255, 4, 0, true, true, 0);
			}
		}
	}
}