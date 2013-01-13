﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Service
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Entities.GoogleMap;
    using Senparc.Weixin.MP.Helpers;

    public class LocationService
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var responseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.News) as
                                ResponseMessageNews;

            var markersList = new List<Markers>();
            markersList.Add(new Markers()
            {
                X = requestMessage.Location_X,
                Y = requestMessage.Location_Y,
                Color = "red",
                Label = "S",
                Size = MarkerSize.Default,
            });
            var mapSize = "480x600";
            var mapUrl = GoogleMapHelper.GetGoogleStaticMap(requestMessage.Scale, markersList, size: mapSize);
            responseMessage.Articles.Add(new Article()
                                             {
                                                 Description = requestMessage.Label,
                                                 PicUrl = mapUrl,
                                                 Title = "定位地点周边地图",
                                                 Url = mapUrl
                                             });
            responseMessage.Articles.Add(new Article()
            {
                Title = "微信公众平台SDK 官网链接",
                Description = "Senparc.Weixin.MK SDK地址",
                PicUrl = "http://weixin.senparc.com/images/logo.jpg",
                Url = "http://weixin.senparc.com"
            });

            responseMessage.Content =
                string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                              requestMessage.Location_X, requestMessage.Location_Y,
                              requestMessage.Scale, requestMessage.Label);
            return responseMessage;
        }
    }
}