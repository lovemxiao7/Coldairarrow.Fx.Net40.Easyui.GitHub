using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Web.Mvc;

namespace Coldairarrow.Web
{
    public class Base_AppSecretController : BaseMvcController
    {
        Base_AppSecretBusiness _base_AppSecretBusiness = new Base_AppSecretBusiness();

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_AppSecret() : _base_AppSecretBusiness.GetTheData(id);

            return View(theData);
        }

        public ActionResult PermissionForm(string appId)
        {
            ViewData["appId"] = appId;

            return View();
        }

        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _base_AppSecretBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
        public ActionResult SaveData(Base_AppSecret theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();

                _base_AppSecretBusiness.AddData(theData);

                WriteSysLog($"����Ӧ��Id:{theData.AppId}", EnumType.LogType.�ӿ���Կ����);
            }
            else
            {
                _base_AppSecretBusiness.UpdateData(theData);
                WriteSysLog($"����Ӧ��Id:{theData.AppId}", EnumType.LogType.�ӿ���Կ����);
            }

            return Success();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            var idList = ids.ToList<string>();
            _base_AppSecretBusiness.DeleteData(idList);
            WriteSysLog($"ɾ����Ȼ����Ϊ:{string.Join(",", idList)}��Ӧ��Id����", EnumType.LogType.�ӿ���Կ����);

            return Success("ɾ���ɹ���");
        }

        public ActionResult SavePermission(string appId, string permissions)
        {
            PermissionManage.SetAppIdPermission(appId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}