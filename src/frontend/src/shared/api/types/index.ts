export type ApiResponseErrors = Map<string, string[]>

export interface ApiResponse<TSuccess = undefined> {
    isSuccess: boolean
    success: TSuccess
    errors: ApiResponseErrors
}
