﻿using System.Management.Automation;
using UniversalDashboard.Models;

namespace UniversalDashboard.Cmdlets.Inputs
{
	[Cmdlet(VerbsCommon.New, "UDInputAction")]
    public class NewInputActionCommand : PSCmdlet
    {
		[Parameter(Mandatory = true, ParameterSetName = "toast")]
		public string Toast { get; set; }

	    [Parameter(ParameterSetName = "toast")]
	    public int Duration { get; set; } = 1000;

	    [Parameter(Mandatory = true, ParameterSetName = "redirect")]
		public string RedirectUrl { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = "content")]
		public Component[] Content { get; set; }

		[Parameter(ParameterSetName = "toast")]
		public SwitchParameter ClearInput { get; set; }

		protected override void EndProcessing()
	    {
			var inputAction = new InputAction();

		    if (ParameterSetName == "toast")
		    {
			    inputAction.Type = InputAction.Toast;
			    inputAction.Text = Toast;
				inputAction.ClearInput = ClearInput;
			    inputAction.Duration = Duration;
		    }

		    if (ParameterSetName == "redirect")
		    {
			    inputAction.Type = InputAction.Redirect;
			    inputAction.Route = RedirectUrl;
		    }

			if (ParameterSetName == "content")
			{
				inputAction.Type = InputAction.Content;
				inputAction.Components = Content;
			}

			WriteObject(inputAction);

	    }
    }
}
