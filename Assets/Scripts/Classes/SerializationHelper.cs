using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SerializationHelper
{
   public static byte[] Serialise<T>(T input) {
       byte[] output = null;
       //XML 포맷터를 생성한다
       var serializer = new XmlSerializer(typeof(T));
       try {
           //직렬화 결과물을 들고 있기 위한 메모리 스트림 생성
           using(var stream = new MemoryStream()) {

               //데이터 직렬화
               serializer.Serialize(stream, input);
               //직렬화 결과물 가져오기
               output = stream.GetBuffer();

           }
           
       } catch{}
       //직렬화 된 결과물 반환
       return output;
   }

   public static T Deserialise<T>(Stream input) {
       T output = default(T);
        //XML 포맷터 생성
        var serializer = new XmlSerializer(typeof(T));
        try {
            //스트림으로부터 데이터를 역직렬화 한다
            output = (T)serializer.Deserialize(input);
        } catch{}
        //역직렬화된 결과물을 반환한다
        return output;
   }

   public static T Deserialise<T>(byte[] input) {
       T output = default(T);
       //XML 포맷터 생성
       var serializer = new XmlSerializer(typeof(T));
       try {
           //직렬화된 데이터를 갖는 인메모리 스트림 생성
           using(var stream = new MemoryStream(input)) {
               //스트림으로부터 데이터 역직렬화
               output = (T) serializer.Deserialize(stream);
           }
       } catch{

       } 
        //역직렬화 데이터 반환
        return output;
   }
}
