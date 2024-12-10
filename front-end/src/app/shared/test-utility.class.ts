import { ApiResponse } from './types';

export class TestUtility {
  static successResponse<TData>(data: TData): ApiResponse<TData> {
    return {
      success: true,
      statusCode: 200,
      message: '',
      data
    };
  }
}
