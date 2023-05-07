﻿using Newtonsoft.Json;

namespace Chat_Server.Helpers {
	public static class JsonSerializeHelper {
		// todo(v): используй новый вид инициализатора
		private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings {
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Ignore
		};

		public static string Serialize(object obj) {
			return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
		}

		public static TKey Deserialize<TKey>(string obj) {
			return JsonConvert.DeserializeObject<TKey>(obj);
		}

		public static bool TryDeserialize<TKey>(string obj, out TKey result) {
			try {
				result = JsonConvert.DeserializeObject<TKey>(obj);
				return true;
			}
			catch {
				result = default;
				return false;
			}
		}
	}
}