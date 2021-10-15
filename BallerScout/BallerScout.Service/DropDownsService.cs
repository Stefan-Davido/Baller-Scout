using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service
{
    public class DropDownsService : IDropDownsService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPlayerHistoryService _playerHistoryService;

        public DropDownsService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPlayerHistoryService playerHistoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _playerHistoryService = playerHistoryService;
        }

        public async Task<List<SelectListItem>> Foot(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userFoot = user.Foot;
            List<string> chooseFoot = new List<string>();
            chooseFoot.Add("Left");
            chooseFoot.Add("Right");
            chooseFoot.Add("Both");
            var counter = 0;

            List<SelectListItem> footSelectList = new List<SelectListItem>();

            foreach (var foot in chooseFoot)
            {
                if(foot == userFoot)
                {
                    footSelectList.Add
                    (
                        new SelectListItem
                        {
                            Text = foot,
                            Value = foot,
                            Selected = true
                        }
                    );
                    counter++;
                }
                else
                {
                    footSelectList.Add
                    (
                        new SelectListItem
                        {
                            Text = foot,
                            Value = foot,
                        }
                    );
                }
            }

            if(counter == 0)
            {
                footSelectList.Add(new SelectListItem() { Text = "Select Foot:", Value = "0", Selected = true });
            }

            return footSelectList;
        }

        public List<SelectListItem> TransferType(int transferId)
        {
            var playerHistory = _playerHistoryService.GetPlayerHistoryById(transferId);
            List<string> chooseTransferType = new List<string>();
            chooseTransferType.Add("Free");
            chooseTransferType.Add("Official");
            chooseTransferType.Add("Loan");
            var counter = 0;

            List<SelectListItem> transferList = new List<SelectListItem>();

            foreach (var transfer in chooseTransferType)
            {
                if (playerHistory == null)
                {
                    transferList.Add
                    (
                        new SelectListItem
                        {
                            Text = transfer,
                            Value = transfer,
                        }
                    );
                }
                else if(transfer == playerHistory.ContractType && playerHistory != null )
                {
                    transferList.Add
                    (
                        new SelectListItem
                        {
                            Text = transfer,
                            Value = transfer,
                            Selected = true
                        }
                    );
                    counter++;
                }
                else
                {
                    transferList.Add
                    (
                        new SelectListItem
                        {
                            Text = transfer,
                            Value = transfer,
                        }
                    );
                }
            }

            if (counter == 0)
            {
                transferList.Add(new SelectListItem() { Text = "Select Transfer Type:", Value = "0", Selected = true });
            }

            return transferList;
        }

        public async Task<List<SelectListItem>> Postition(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userPosition = user.Position;

            var positionList = PositionObject();
            var counter = 0;
            List<SelectListItem> positionSelectList = new List<SelectListItem>();

            foreach (var position in positionList)
            {
                if(position.ShortName == userPosition)
                {                   
                    positionSelectList.Add
                    (
                        new SelectListItem
                        { 
                            Text = position.LongName,
                            Value = position.ShortName,
                            Selected = true
                        }
                    );

                    counter++;
                }
                else
                {
                    positionSelectList.Add
                    (
                        new SelectListItem 
                        { 
                            Text = position.LongName,
                            Value = position.ShortName
                        }
                    );
                }
               
            }

            if(counter == 0)
            {
                positionSelectList.Add
                (
                    new SelectListItem() 
                    {
                        Text = "Select Position:",
                        Value = "0",
                        Selected = true 
                    }
                );
            }

            return positionSelectList;
        }

        private  List<string> PlayerPosition()
        {
            List<string> playerPosition = new List<string>();
            playerPosition.Add("GK-Goalkeeper");
            playerPosition.Add("LB-Left Back");
            playerPosition.Add("RB-Right Back");
            playerPosition.Add("CB-Central Back");
            playerPosition.Add("LWB-Left Wing Back");
            playerPosition.Add("RWB-Right Wing Back");
            playerPosition.Add("CAM-Central Attack Middle");
            playerPosition.Add("RM-Right Middle");
            playerPosition.Add("LM-Left Middle");
            playerPosition.Add("CM-Central Middle");
            playerPosition.Add("AM-Attack Middle");
            playerPosition.Add("CDM-Central Defense Middle");
            playerPosition.Add("LW-Left Wing");
            playerPosition.Add("RW-Right Wing");
            playerPosition.Add("ST-Striker");
            playerPosition.Add("CF-Central Forward");
            playerPosition.Add("RF-Right Forward");
            playerPosition.Add("LF-Left Forward");

            return playerPosition;
        }

        private List<PositionObject> PositionObject()
        {          
            List<PositionObject> positionObjects = new List<PositionObject>();
            var positionList = PlayerPosition();

            for (int i = 0; i <= positionList.Count()-1; i++)
            {
                PositionObject posObj = new PositionObject();
                string[] splitet = positionList[i].Split("-");
                posObj.ShortName = splitet[0];
                posObj.LongName = splitet[1];
                posObj.Id = i;
                positionObjects.Add(posObj);
            }

            return positionObjects;
        }
    }

    public class PositionObject
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }
}
