﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL_DOCTORCLIENT.Services;

namespace TES_MEDICAL_DOCTORCLIENT.Pages
{
    public partial class PhieuKhamPage
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IModal modal { get; set; }
        [Inject]

        public NavigationManager navigationManager { get; set; }
        [Inject]
        ILocalStorageService localstorage { get; set; }
        [Inject]
        IKhamBenh khambenhRep { get; set; }
        public List<PhieuKham> PhieuKhams { get; set; } = new List<PhieuKham>();
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
           


             PhieuKhams = await khambenhRep.GetPhieuKham(Guid.Parse("9B208B13-6D00-4367-B736-B0DF8F600BAE"));


           
        }
    }
}