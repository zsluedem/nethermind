//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

namespace Nethermind.Overseer.Test.JsonRpc
{
    public class JsonRpcResponse<T>
    {
        public int Id { get; set; }
        public string JsonRpc { get; set; }
        public T Result { get; set; }
        public ErrorResponse Error { get; set; }
        public bool IsValid => Error is null;

        public class ErrorResponse
        {
            public ErrorResponse(int code, string message, object data = null)
            {
                Code = code;
                Message = message;
            }

            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }
    }
}
