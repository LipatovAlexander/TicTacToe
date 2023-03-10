import { notification } from 'antd'
import { createEffect, createEvent, forward } from 'effector'

export const success = createEvent<string>()
export const error = createEvent<string>()
export const info = createEvent<string>()

const notifySuccess = createEffect((message: string) => {
    notification.success({
        message: message,
        placement: 'bottomRight',
    })
})

const notifyError = createEffect((message: string) => {
    notification.error({
        message: message,
        placement: 'bottomRight',
    })
})

const notifyInfo = createEffect((message: string) => {
    notification.info({
        message: message,
        placement: 'bottomRight',
    })
})

forward({
    from: success,
    to: notifySuccess,
})

forward({
    from: error,
    to: notifyError,
})

forward({
    from: info,
    to: notifyInfo,
})
