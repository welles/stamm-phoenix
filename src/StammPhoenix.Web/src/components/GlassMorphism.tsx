import { type Component, mergeProps } from 'solid-js'

const GlassMorphism: Component<{
	class?: string
	id?: string
	children?: any
}> = (props) => {
	props = mergeProps({ class: '', id: '' }, props)
	return (
		<div
			id={props.id}
			class={`m-[1rem] p-[0.5rem] bg-gray-300 rounded-lg bg-clip-padding backdrop-filter backdrop-blur-lg bg-opacity-30 border ${props.class}`}
		>
			{props.children}
		</div>
	)
}

export default GlassMorphism
