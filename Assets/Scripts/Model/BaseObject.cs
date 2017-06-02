using UnityEngine;
using System.Collections;
using Sfs2X.Entities.Data;

public class BaseObject {

	public string _error;
	public string _cmd;
	public ISFSObject _param;

	public BaseObject (string cmd, ISFSObject param){
		_cmd = cmd;
		_param = param;
	}

	public string Error{
		get{
			return _error;
		}
		set{
			_error = value;
		}
	}

	public string Cmd{
		get{
			return _cmd;
		}
		set{
			_cmd = value;
		}
	}

	public ISFSObject Param{
		get{
			return _param;
		}
		set{
			_param = value;
		}
	}

}
